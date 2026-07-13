/*
[df:title]
RawData、FAデータ Delete

[df:description]
RawDataテーブル、FAデータテーブルのデータ削除を行います。

TSurveyDataBhv_Delete.sql
*/
-- !TSurveyDataDeletePmb!
-- !!string TableName!!
-- !!string SampleId!!

DELETE FROM /*$pmb.TableName*/T_QCWEBID_01 WHERE SAMPLE_ID LIKE /*pmb.SampleId*/'1%'
