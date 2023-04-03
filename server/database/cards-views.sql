CREATE OR REPLACE VIEW cards.Repeats AS
SELECT 
random() AS "Random",
d."SideType" as "SideType",
d."CardId" as "CardId",
q."Label" as "Question",
a."Label" as "Answer",
q."Example" as "QuestionExample",
a."Example" as "AnswerExample",
d."IsQuestion" as "LessonIncluded",
d."NextRepeat" as "NextRepeat",
d."Drawer" as "QuestionDrawer",
case when d."SideType" = 1 then g."Front" else g."Back" end as "QuestionLanguage",
case when d."SideType" = 1 then g."Back" else g."Front" end as "AnswerLanguage",
o."UserId" as "UserId",
g."Id" as "GroupId"
from cards."Details" d
join cards."Cards" c ON c."Id" = d."CardId"
join cards."Sides" q ON (q."Id" = c."BackId" and d."SideType" = 2) OR (q."Id" = c."FrontId" and d."SideType" = 1)
join cards."Sides" a ON (a."Id" = c."FrontId" and d."SideType" = 2) OR (a."Id" = c."BackId" and d."SideType" = 1)
join cards."Groups" g ON g."Id" = c."GroupId"
join cards."Owners" o ON o."Id" = g."OwnerId"
ORDER BY 1;

CREATE OR REPLACE VIEW cards.grouptolesson AS
select 
o."UserId" as "OwnerId",
g."Id" AS "Id",
g."Name" AS "Name",
g."Front" AS "Front",
g."Back" AS "Back",
COUNT(CASE f."IsQuestion" when true then null else 1 end) as "FrontCount",
COUNT(CASE b."IsQuestion" when true then null else 1 end) as "BackCount"
from cards."Groups" g
join cards."Owners" o ON o."Id" = g."OwnerId"
join cards."Cards" c ON c."GroupId" = g."Id"
join cards."Details" f ON f."CardId" = c."Id" and f."SideType" = 1
join cards."Details" b ON b."CardId" = c."Id" and b."SideType" = 2
group by g."Id", o."UserId";



CREATE OR REPLACE VIEW cards.repeatscountsummary
 AS
 SELECT count(0) AS "Count",
    date_trunc('day'::text, d."NextRepeat") AS "Date",
    o."UserId" AS "OwnerId"
   FROM cards."Details" d
     JOIN cards."Cards" ON "Cards"."Id" = d."CardId"
     JOIN cards."Groups" ON "Groups"."Id" = "Cards"."GroupId"
     JOIN cards."Owners" o ON o."Id" = "Groups"."OwnerId"
  GROUP BY "Date", o."UserId"
  ORDER BY "Date";


CREATE OR REPLACE VIEW cards.cardsIndex
  AS
  SELECT
  o."UserId" AS "UserId",
  c."Id" as "CardId",
  g."Id" as "GroupId",
  g."Name" AS "GroupName",
  g."Front" AS "Front",
  g."Back" AS "Back",
  f."Label" AS "FrontValue",
  f."Example" AS "FrontExample",
  b."Label" AS "BackValue",
  b."Example" AS "BackExample",
  df."Drawer" AS "FrontDrawer",
  df."IsQuestion" AS "FrontIsQuestion",
  df."IsTicked" AS "FrontIsTicked",
  bd."Drawer" AS "BackDrawer",
  bd."IsQuestion" AS "BackIsQuestion",
  bd."IsTicked" AS "BackIsTicked"
  FROM cards."Cards" c
  RIGHT JOIN cards."Details" df ON df."CardId" = c."Id" AND df."SideType" = 1
  RIGHT JOIN cards."Details" bd ON bd."CardId" = c."Id" AND bd."SideType" = 2
  JOIN cards."Sides" f ON f."Id" = c."FrontId"
  JOIN cards."Sides" b ON b."Id" = c."BackId"
  JOIN cards."Groups" g ON g."Id" = c."GroupId"
  JOIN cards."Owners" o ON o."Id" = g."OwnerId";