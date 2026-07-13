/*
[df:title]
RAWDATA作成

[df:description]
RAWDATA作成

以下の条件値は必須です
・wkTbl
・wkCol
・wkColVal

TSurveyDataBhv_CreateRawdataLine.sql

*/

-- !TRawDataCreateLinePmb!
-- !!String FromTable!!
-- !!String FromField!!
-- !!String ToTable!!
-- !!String ToField!!
-- !!String SampleIdField!!
-- !!String DataField!!

MERGE INTO /*$pmb.ToTable*/T_20000_02 TOTB
USING (SELECT /*pmb.SampleIdField*/'STRING0' AS SAMPLE_ID,/*pmb.DataField*/'STRING1' AS FROMCOL FROM DUAL) FROMTB
ON ( FROMTB.SAMPLE_ID = TOTB.SAMPLE_ID)
-- 既存レコードの更新
WHEN MATCHED THEN
	UPDATE SET
		TOTB./*$pmb.ToField*/C0008 = /*pmb.DataField*/'STRING1'
-- 新規レコードの作成
WHEN NOT MATCHED THEN
	INSERT
		( SAMPLE_ID, /*$pmb.ToField*/C0008 )
	VALUES
		( /*pmb.SampleIdField*/'STRING0',
          /*pmb.DataField*/'STRING1' )
