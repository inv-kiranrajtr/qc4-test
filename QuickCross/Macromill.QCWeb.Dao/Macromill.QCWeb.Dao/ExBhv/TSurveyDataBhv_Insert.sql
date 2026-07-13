/*
[df:title]
RawData、FAデータ Insert

[df:description]
RawDataテーブル、FAデータテーブルに登録を行います。

TSurveyDataBhv_Insert.sql
*/
-- !TSurveyDataInsertPmb!
-- !!string TableName!!
-- !!string SampleId!!
-- !!string InsertStr!!
-- !!string InsertVal!!
-- !!string Hint!!

INSERT /*$pmb.Hint*/Hint INTO /*$pmb.TableName*/T_QCWEBID_01
	( SAMPLE_ID,/*$pmb.InsertStr*/INSERT_COLUMNS )
VALUES
	( /*pmb.SampleId*/'1',/*$pmb.InsertVal*/INSERT_VALUES )
