/*
[df:title]
1.5.RawData
非該当・無回答取得

[df:description]
データ件数を取得します
テーブル名、カラム名およびカラム名の設定値は任意設定が可能です

以下の条件値は必須です
・wkTbl
・wkCol
・wkColVal

$Id: TSurveyDataBhv_abstractSelectCount.sql 5718 2012-03-21 09:23:29Z tkubota@JOPS.LOCAL $
*/
-- #RawDataEntity#
-- !TRawDataPmb!
-- !!String wkTbl!!
-- !!String wkCol!!
-- !!String wkColVal!!

SELECT
	COUNT(*) AS ROW_COUNT
FROM
	/*$pmb.WkTbl*/T_Survey_Data RAWDATA
WHERE
	RAWDATA./*$pmb.WkCol*/Sample_ID = /*pmb.WkColVal*/'1'
