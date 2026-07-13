/*
[df:title]
セレクト条件情報 Delete

[df:description]
QCWeb管理IDを指定してセレクト条件情報テーブルのデータ削除を行います。

TSelectConditionInfoBhv_Delete.sql
*/
-- !TSelectConditionInfoDeletePmb!
-- !!decimal Qcwebid!!

DELETE FROM T_SELECT_CONDITION_INFO WHERE QCWEBID = /*pmb.Qcwebid*/1
