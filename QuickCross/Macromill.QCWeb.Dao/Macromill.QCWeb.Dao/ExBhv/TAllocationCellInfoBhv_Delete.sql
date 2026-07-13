/*
[df:title]
割付セル情報 Delete

[df:description]
QCWeb管理IDを指定して割付セル情報テーブルのデータ削除を行います。

TAllocationCellInfoBhv_Delete.sql
*/
-- !TAllocationCellInfoDeletePmb!
-- !!decimal Qcwebid!!

DELETE FROM T_ALLOCATION_CELL_INFO WHERE QCWEBID = /*pmb.Qcwebid*/1
