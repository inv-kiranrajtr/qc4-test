/*
[df:title]
FAデータ
指定テーブル、カラムのFAデータを取得する

[df:description]
指定データの全件を取得する。
テーブル名、カラム名の設定値は任意設定が可能です


以下の条件値は必須です
・FromTable
・FromField

$Id: TFaDataBhv_SelectFaDataItemViewFAList.sql 5718 2012-04-13 hsasaki@JOPS.LOCAL $
*/
-- #FaDataItemViewFAListEntity#

-- !TFaDataItemViewFAListPmb!
-- !!String FromTable!!
-- !!String FromField!!

SELECT
    SAMPLE_ID AS SAMPLE_ID,
	/*$pmb.FromField*/'FA001' AS RawData
FROM
	/*$pmb.FromTable*/T_SURVEY_DATA
