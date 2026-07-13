/*
[df:title]
シナリオテーブルのウエイトバック設定有無フラグ更新（Loadバッチの途中集計特殊処理用）

[df:description]
指定されたアイテムIDを利用しているシナリオのウエイトバック設定有無フラグをOFFにする。

TScenarioTotalizationBhv_UpdateWeightbackFlag.sql
*/
-- !TScenarioUpdateWeightbackFlagPmb!
-- !!decimal Qcwebid!!
-- !!decimal ItemInfoId!!
-- !!string LastUpdateUser!!
-- !!DateTime LastUpdateDatetime!!

UPDATE
	T_SCENARIO_TOTALIZATION		T1
SET
	WEIGHTBACK_FLAG = 0,
	LAST_UPDATE_USER = /*pmb.LastUpdateUser*/'system',
	LAST_UPDATE_DATETIME = /*pmb.LastUpdateDatetime*/SYSDATE
WHERE
	QCWEBID = /*pmb.Qcwebid*/100
AND	(
		EXISTS(	SELECT 'X'
				FROM
					T_GT_SCENARIO_ITEM			T2
				WHERE
					T1.SCENARIO_TOTALIZATION_ID = T2.SCENARIO_TOTALIZATION_ID
				AND T2.ITEM_INFO_ID = /*pmb.ItemInfoId*/200	)
		OR
		EXISTS(	SELECT 'X' 
				FROM
					T_CROSS_SCENARIO_TARGET		T3,
					T_CROSS_SCENARIO_ITEM		T4
				WHERE
					T1.SCENARIO_TOTALIZATION_ID = T3.SCENARIO_TOTALIZATION_ID
				AND T3.CROSS_SCENARIO_TARGET_ID = T4.CROSS_SCENARIO_TARGET_ID
				AND (	T4.AXIS1_ITEM_ID = /*pmb.ItemInfoId*/200
					OR	T4.AXIS2_ITEM_ID = /*pmb.ItemInfoId*/200	))
	)
