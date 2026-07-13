/*
[df:title]
RawDataのテーブルコピーを行います

[df:description]
指定されたテーブルに1番テーブルのサンプルIDのみをコピーし初期化します。

以下の条件値は必須です
・FromTable
・ToTable

$Id: TSurveyDataBhv_InitRawDataTable.sql 5720 2012-04-20 mkomatsu@JOPS.LOCAL $
*/

-- !TRawDataCopyPmb!
-- !!String FromTable!!
-- !!String ToTable!!

INSERT INTO /*$pmb.ToTable*/T_20000_02
(SAMPLE_ID) SELECT SAMPLE_ID FROM /*$pmb.FromTable*/T_20000_01
