/*
[df:title]
アイテム情報、カテゴリ情報、RawDataコントロール情報を取得する

[df:description]
QCWeb管理ID、アイテムIDを指定して検索します
以下の条件値は必須です
・QCWebID
・ItemInfoID

$Id: TItemInfoBhv_SelectQuestionsCategoryInfo.sql 3089 2012-07-18 08:57:39Z cterash $ / $Date: 2012-07-18 17:57:39 +0900 (2012/07/18 (水)) $ / $Rev: 3089 $ / $Author: cterash $

*/
-- #TItemInfoQuestionsCategory#
-- !TItemInfoQuestionsCategoryPmb!
-- !!Decimal ItemInfoId!!

SELECT
    ITEM.ITEM_INFO_ID
    ,ITEM.QCWEBID
    ,ITEM.ITEM_NAME
    ,ITEM.SOURCE_DIV
    ,ITEM.ITEMNO
    ,ITEM.ITEM_TYPE
    ,ITEM.ANSWER_TYPE
    ,ITEM.MATRIX_DIV
    ,ITEM.LV1TITLE
    ,ITEM.LV2TITLE
    ,ITEM.ORIGINAL_LV1TITLE
    ,ITEM.ORIGINAL_LV2TITLE
    ,ITEM.TABLE_NAME
    ,ITEM.COLUMN_NAME
    ,ITEM.STATUS
    ,ITEM.SORT_FLAG
	,ITEM.Sort_Range
    ,ITEM.LAST_UPDATE_DATETIME
	,CATEGORY.Category_No									
	,CATEGORY.Category_Name									
	,CATEGORY.Weight_Value									
	,CATEGORY.Original_Category_Name									
    ,MATRIX.ITEM_INFO_ID as Parent_ItemInfo_Id
    ,MATRIX.ADD_FA_CATEGORY_INFO_ID
	,TBL.BASE_TABLE_NAME
FROM
	T_ITEM_INFO ITEM LEFT JOIN T_MATRIX_INFO MATRIX ON
		ITEM.ITEM_INFO_ID = MATRIX.CHILD_ITEM_INFO_ID
	LEFT JOIN T_CATEGORY_INFO CATEGORY ON
		ITEM.ITEM_INFO_ID = CATEGORY.ITEM_INFO_ID
	INNER JOIN T_TABLE_CONTROL TBL ON
		ITEM.QCWEBID = TBL.QCWEBID
WHERE
	    ITEM.ITEM_INFO_ID = /*pmb.ItemInfoId*/342
	AND ITEM.STATUS = 1
ORDER BY
	CATEGORY.Category_No asc
;