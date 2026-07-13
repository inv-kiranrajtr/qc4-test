/*
[df:title]
アイテム情報を取得する

[df:description]
QCWeb管理IDを指定して検索します
以下の条件値は必須です
・QCWebID
*/
-- #TItemInfoSimple#
-- !TItemInfoSimplePmb!
-- !!Decimal QCWebId!!

SELECT
    ITEM.ITEM_INFO_ID
    ,ITEM.ITEM_NAME
	,ITEM.DATA_EDIT_ID
    ,ITEM.STATUS
FROM
	T_ITEM_INFO ITEM 
WHERE
		ITEM.QCWEBID = /*pmb.QCWebId*/28
ORDER BY
	ITEM.SORT_NUMBER asc
	,ITEM.ITEM_INFO_ID asc
;