/*
[df:title]
QCWeb調査管理の追加データ管理番号の採番

[df:description]
QCWeb調査管理の追加データ管理番号をシーケンスより採番します

TQcwebSurveyInfoBhv_selectAddDataNoNextVal.sql
*/

-- #QcwebSurveyInfoSelectAddDataNoNextValEntity#
-- !TQcwebSurveyInfoSelectAddDataNoNextValPmb!

SELECT T_QCWEB_SURVEY_INFO_SEQ_02.nextval FROM DUAL
