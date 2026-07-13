/*
[df:title]
RawData
指定テーブル、カラムのRAWDATAを取得する

[df:description]
指定データの全件を取得する。
テーブル名、カラム名の設定値は任意設定が可能です


以下の条件値は必須です
・FromTable
・FromField

$Id: TSurveyDataBhv_SelectRawData.sql 5718 2012-04-13 hsasaki@JOPS.LOCAL $
*/
-- #RawDataItemViewFAListEntity#

-- !TRawDataItemViewFAListPmb!
-- !!String FromTable!!
-- !!String FromField!!

SELECT
    SAMPLE_ID AS SAMPLE_ID,
	/*$pmb.FromField*/Q0001 AS RawData
FROM
	/*$pmb.FromTable*/T_SURVEY_DATA
