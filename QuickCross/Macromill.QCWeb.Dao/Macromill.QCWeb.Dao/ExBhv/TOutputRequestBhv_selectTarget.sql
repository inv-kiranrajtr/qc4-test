/*
[df:title]
出力物削除処理に先立ち、出力物作成依頼情報を検索

[df:description]
出力物削除バッチで利用。仕様書 2.3.1 の SQL 文。

TOutputRequestBhv_selectTarget.sql
*/

-- #TOutputRequestEntity#
-- !TOutputRequestPmb!
-- !!DateTime RequestDateTime!!
-- !!DateTime TestLogDateTime!!

SELECT
	SUB.Output_Reportset_Info_ID
	, SUB.Download_Path
	, SUB.Output_Request_ID
FROM
	(
		SELECT
			Output_Reportset_Info_ID
			, Download_Path
			, Output_Request_ID
		FROM
			T_Output_Request
		WHERE
			(
				(Request_DateTime <= /*pmb.RequestDateTime*/SYSDATE AND Delete_Flag = 0  /* off */)
				OR Delete_Flag = 1
			)
			AND TEST_LOG_FLAG = 0
	UNION ALL
		SELECT
			Output_Reportset_Info_ID
			, Download_Path
			, Output_Request_ID
		FROM
			T_Output_Request
		WHERE
			(
				(Request_DateTime <= /*pmb.TestLogDateTime*/SYSDATE AND Delete_Flag = 0  /* off */)
				OR Delete_Flag = 1
			)
			AND TEST_LOG_FLAG = 1
	) SUB
ORDER BY
  SUB.Output_Request_ID ASC
;
