CREATE OR REPLACE VIEW cards.Repeats AS
select 
q."CardId" as "CardId",
q."Value" as "QuestionValue",
q."Example" as "QuestionExample",
q."Side" as "QuestionSide",
a."Value" as "AnswerValue",
a."Example" as "AnswerExample",
a."Side" as "AnswerSide",
c."Comment" as "Comment",
g."FrontLanguage" as "FrontLanguage",
g."BackLanguage" as "BackLanguage",
s."UserId" as "UserId"
from cards."CardSides" q
join cards."CardSides" a on q."CardId" = a."CardId" and q."Side" <> a."Side"
join cards."Cards" c ON c."Id" = q."CardId"
join cards."Groups" g ON g."Id" = c."GroupId"
join cards."Sets" s ON s."Id" = g."CardsSetId"
where q."IsUsed" = true
order by q."NextRepeat";

