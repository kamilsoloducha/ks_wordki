CREATE OR REPLACE VIEW cards.Repeats AS
select 
q."CardId" as "CardId",
q."Value" as "QuestionValue",
q."Example" as "QuestionExample",
q."Side" as "QuestionSide",
case when q."Side" = 1 then g."FrontLanguage" else g."BackLanguage" end as "QuestionLanguage",
q."NextRepeat" as "NextRepeat",
a."Value" as "AnswerValue",
a."Example" as "AnswerExample",
case when a."Side" = 1 then g."FrontLanguage" else g."BackLanguage" end as "AnswerLanguage",
a."Side" as "AnswerSide",
c."Comment" as "Comment",
s."UserId" as "UserId",
g."Id" as "GroupId",
q."IsUsed" as "IsUsed"
from cards."CardSides" q
join cards."CardSides" a on q."CardId" = a."CardId" and q."Side" <> a."Side"
join cards."Cards" c ON c."Id" = q."CardId"
join cards."Groups" g ON g."Id" = c."GroupId"
join cards."Sets" s ON s."Id" = g."CardsSetId"
order by q."NextRepeat";
