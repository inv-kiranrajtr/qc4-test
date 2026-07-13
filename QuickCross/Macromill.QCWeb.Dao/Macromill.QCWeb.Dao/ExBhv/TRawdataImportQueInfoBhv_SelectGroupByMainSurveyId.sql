/*
[df:title]
取込管理情報を本調査ID単位で検索

[df:description]
本調査IDでGROUP BYして取得します

TRawdataImportQueInfoBhv_SelectGroupByMainSurveyId.sql
*/

-- #RawdataImportQueInfoGroupByEntity#
-- !TRawdataImportQueInfoGroupByPmb!
-- !!int ImportStatus!!

SELECT
	MAIN_SURVEY_ID
FROM
	T_RAWDATA_IMPORT_QUE_INFO
WHERE
	IMPORT_STATUS = /*pmb.ImportStatus*/0
GROUP BY
	MAIN_SURVEY_ID
ORDER BY
	MAIN_SURVEY_ID
