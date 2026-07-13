/*
[df:title]
RAWDATA作成

[df:description]
RAWDATA作成

以下の条件値は必須です
・wkTbl
・wkCol
・wkColVal

TSurveyDataBhv_CreateRawdata.sql

*/

-- !TRawDataCreatePmb!
-- !!String FromTable!!
-- !!String FromField!!
-- !!String ToTable!!
-- !!String ToField!!

MERGE INTO /*$pmb.ToTable*/T_20000_01 TOTB
USING /*$pmb.FromTable*/T_20000_02 FROMTB
ON ( FROMTB.SAMPLE_ID = TOTB.SAMPLE_ID)
-- 既存レコードの更新
WHEN MATCHED THEN
	UPDATE SET
		TOTB./*$pmb.ToField*/C0008 = FROMTB./*$pmb.FromField*/C0002
-- 新規レコードの作成
WHEN NOT MATCHED THEN
	INSERT
		( SAMPLE_ID, /*$pmb.ToField*/C0008 )
	VALUES
		( FROMTB.SAMPLE_ID,
          FROMTB./*$pmb.FromField*/C0002 )
