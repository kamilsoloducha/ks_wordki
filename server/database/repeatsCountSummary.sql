CREATE OR REPLACE VIEW cards.RepeatsCountSummary AS
SELECT
COUNT(0) as Count,
c."NextRepeat" as "Date",
s."UserId" as "UserId"
FROM cards."CardSides" c
JOIN cards."Cards" ON "Cards"."Id" = c."CardId"
JOIN cards."Groups" ON "Groups"."Id" = "Cards"."GroupId"
JOIN cards."Sets" s ON s."Id" = "Groups"."CardsSetId"
GROUP BY c."NextRepeat", s."UserId"
ORDER BY c."NextRepeat"