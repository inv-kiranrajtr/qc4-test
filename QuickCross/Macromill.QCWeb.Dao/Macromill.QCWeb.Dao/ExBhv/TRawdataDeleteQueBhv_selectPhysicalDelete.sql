/*
[df:title]
物理削除対象を検索

[df:description]
物理削除バッチ用に、物理削除対象の情報を検索する。

TRawdataDeletetQueBhv_selectPhysicalDelete.sql
*/

-- #TRawdataDeleteQueEntity#
-- !TRawdataDeleteQuePmb!
-- !!DateTime DeleteOrderDate!!
-- !!DateTime ScheduleDeleteDate!!

SELECT
	1 AS Delete_Kbn
	, rdq.RawData_Delete_Que_ID AS RawData_Delete_Que_ID
	, qsi.QCWebID AS QCWebID
	, rdq.Main_Survey_ID AS Main_Survey_ID
	, NULL AS Survey_Info_ID
FROM
	T_RawData_Delete_Que rdq INNER JOIN T_QCWeb_Survey_Info qsi ON
		rdq.Add_Data_No = qsi.Add_Data_No
WHERE
		rdq.Delete_Status = 2  /* 削除完了 */
	AND rdq.Delete_Order_Date <= /*pmb.DeleteOrderDate*/SYSDATE
	AND qsi.Import_DateTime IS NOT NULL
	AND qsi.Delete_Flag = 1
	AND NOT EXISTS (
		/* 3年削除分は除く */
		SELECT
			*
		FROM
			T_Survey_Info si
		WHERE
				qsi.Survey_Info_Id = si.Survey_Info_Id
			AND si.Delete_Flag = 1  /* 論理削除済み */
			AND si.Schedule_Delete_Date <= /*pmb.ScheduleDeleteDate*/SYSDATE
	)
UNION ALL
SELECT
	2 AS Delete_Kbn
	, NULL AS RawData_Delete_Que_ID
	, qsi.QCWebID AS QCWebID
	, NULL AS Main_Survey_ID
	, si.Survey_Info_ID AS Survey_Info_ID
FROM
	T_QCWeb_Survey_Info qsi INNER JOIN T_Survey_Info si ON
		si.Survey_Info_ID = qsi.Survey_Info_ID
WHERE
		si.Delete_Flag = 1  /* 論理削除済み */
	AND si.Schedule_Delete_Date <= /*pmb.ScheduleDeleteDate*/SYSDATE
;