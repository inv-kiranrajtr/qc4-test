/*
[df:title]
RawData Update

[df:description]
RawDataテーブルを更新します

TSurveyDataBhv_UpdateRawdata.sql
*/
-- !TSurveyDataUpdateRawdataPmb!
-- !!string tableName!!
-- !!string columnName!!
-- !!string updValue!!
-- !!string sampleId!!

UPDATE /*$pmb.tableName*/T_SURVEY_DATA SET
	/*$pmb.columnName*/Q003 = /*pmb.updValue*/'test'
WHERE
	SAMPLE_ID = /*pmb.sampleId*/'12345'
;