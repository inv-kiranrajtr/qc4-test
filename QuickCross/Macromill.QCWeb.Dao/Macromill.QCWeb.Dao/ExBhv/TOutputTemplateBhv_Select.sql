/*
[df:title]
テンプレートテーブルを検索し、削除対象ファイルを取得する

[df:description]
AIRs ファイルサーバのファイル削除処理で発行する SQL 文。仕様書 2.4.3 の検索。

TOutputTemplateBhv_Select.sql
*/

-- #TOutputTemplateEntity#
-- !TOutputTemplatePmb!
-- !!decimal QcWebID!!

SELECT
	req.QCWebID
	, t.Output_Template_ID
	, t.Upload_Path
FROM
	T_Output_Request req INNER JOIN T_Output_Reportset_Info rep ON
		req.Output_Reportset_Info_ID = rep.Output_Reportset_Info_ID
	INNER JOIN T_Output_Template t ON
		rep.Output_Template_ID = t.Output_Template_ID
WHERE
		req.QCWebID = /*pmb.QcWebID*/1
;