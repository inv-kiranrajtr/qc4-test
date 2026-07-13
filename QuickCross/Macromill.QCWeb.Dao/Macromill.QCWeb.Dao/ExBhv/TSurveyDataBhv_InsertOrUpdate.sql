/*
[df:title]
RawData、FAデータ InsertOrUpdate

[df:description]
RawDataテーブル、FAデータテーブルに登録を行います。

TSurveyDataBhv_InsertOrUpdate.sql
*/
-- !TSurveyDataInsertOrUpdatePmb!
-- !!string TableName!!
-- !!string SampleId!!
-- !!string InsertStr!!
-- !!string InsertVal!!
-- !!string UpdateStr!!

MERGE INTO /*$pmb.TableName*/T_QCWEBID_01 T1
USING ( SELECT /*pmb.SampleId*/'1' SAMPLE_ID FROM DUAL ) T2
ON ( T1.SAMPLE_ID = T2.SAMPLE_ID )
-- 既存レコードの更新
WHEN MATCHED THEN
	UPDATE SET
		/*$pmb.UpdateStr*/UPDATE_STRING
-- 新規レコードの作成
WHEN NOT MATCHED THEN
	INSERT
		( SAMPLE_ID,/*$pmb.InsertStr*/INSERT_COLUMNS )
	VALUES
		( T2.SAMPLE_ID,/*$pmb.InsertVal*/INSERT_VALUES )
