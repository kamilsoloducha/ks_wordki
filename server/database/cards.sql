CREATE SCHEMA IF NOT EXISTS cards;

CREATE TABLE cards.owners (
    "Id" uuid NOT NULL,
    CONSTRAINT "PK_owners" PRIMARY KEY ("Id")
);

CREATE TABLE cards.sides (
    "Id" bigint NOT NULL,
    "Type" integer NOT NULL,
    "Value" text NOT NULL,
    "Example" text,
    CONSTRAINT "PK_sides" PRIMARY KEY ("Id")
);

CREATE TABLE cards.groups (
    "Id" bigint NOT NULL,
    "Name" text NOT NULL,
    "Front" integer NOT NULL,
    "Back" integer NOT NULL,
    "OwnerId" uuid NOT NULL,
    CONSTRAINT "PK_groups" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_groups_owners_OwnerId" FOREIGN KEY ("OwnerId") REFERENCES cards.owners ("Id") ON DELETE CASCADE
);

CREATE TABLE cards.cards (
    "Id" bigint NOT NULL,
    "IsPrivate" boolean NOT NULL,
    "FrontId" bigint NOT NULL,
    "BackId" bigint NOT NULL,
    CONSTRAINT "PK_cards" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_cards_sides_BackId" FOREIGN KEY ("BackId") REFERENCES cards.sides ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_cards_sides_FrontId" FOREIGN KEY ("FrontId") REFERENCES cards.sides ("Id") ON DELETE CASCADE
);

CREATE TABLE cards.details (
    "OwnerId" uuid NOT NULL,
    "SideId" bigint NOT NULL,
    "Drawer" integer NOT NULL,
    "Counter" integer NOT NULL,
    "NextRepeat" timestamp without time zone NOT NULL,
    "LessonIncluded" boolean NOT NULL,
    "Comment" text NOT NULL,
    "IsTicked" boolean NOT NULL,
    CONSTRAINT "PK_details" PRIMARY KEY ("OwnerId", "SideId"),
    CONSTRAINT "FK_details_owners_OwnerId" FOREIGN KEY ("OwnerId") REFERENCES cards.owners ("Id") ON DELETE CASCADE
);

CREATE TABLE cards.groups_cards (
    "CardsId" bigint NOT NULL,
    "GroupsId" bigint NOT NULL,
    CONSTRAINT "PK_groups_cards" PRIMARY KEY ("CardsId", "GroupsId"),
    CONSTRAINT "FK_groups_cards_cards_CardsId" FOREIGN KEY ("CardsId") REFERENCES cards.cards ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_groups_cards_groups_GroupsId" FOREIGN KEY ("GroupsId") REFERENCES cards.groups ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_cards_BackId" ON cards.cards ("BackId");

CREATE INDEX "IX_cards_FrontId" ON cards.cards ("FrontId");

CREATE INDEX "IX_details_OwnerId" ON cards.details ("OwnerId");

CREATE INDEX "IX_details_SideId" ON cards.details ("SideId");

CREATE INDEX "IX_groups_OwnerId" ON cards.groups ("OwnerId");

CREATE INDEX "IX_groups_cards_GroupsId" ON cards.groups_cards ("GroupsId");

CREATE SEQUENCE cards.GroupIdSequence;

CREATE SEQUENCE cards.CardIdSequence;

CREATE SEQUENCE cards.SideIdSequence;

CREATE OR REPLACE VIEW cards.groupsSummary AS
SELECT 
  g."OwnerId" "OwnerId",
  g."Id" "Id",
  g."Name" "Name",
  g."Front" "Front",
  g."Back" "Back",
  COUNT(gc."CardsId") "CardsCount"
FROM cards."groups" g
LEFT JOIN cards.groups_cards gc ON gc."GroupsId" = g."Id"
GROUP BY g."Id";

CREATE OR REPLACE VIEW cards.Repeats AS
SELECT 
random() AS "Random",
d."OwnerId" as "OwnerId",
d."SideId" as "SideId",
c."Id" as "CardId",
d."LessonIncluded" as "LessonIncluded",
d."NextRepeat" as "NextRepeat",
d."Drawer" as "QuestionDrawer",
q."Value" AS "Question",
q."Example" as "QuestionExample",
q."Type" as "QuestionType",
case when q."Type" = 1 then g."Front" else g."Back" end as "QuestionLanguage",
a."Value" as "Answer",
a."Example" as "AnswerExample",
a."Type" as "AnswerType",
case when q."Type" = 1 then g."Back" else g."Front" end as "AnswerLanguage",
d."Comment" as "Comment",
g."Name" as "GroupName",
g."Front" as "FrontLanguage",
g."Back" as "BackLanguage",
g."Id" as "GroupId"
FROM cards.details d
JOIN cards.sides q ON q."Id" = d."SideId"
left join cards.cards c ON (q."Type" = 1 and c."FrontId" = q."Id") or (q."Type" = 2 and c."BackId" = q."Id")
join cards.sides a on (q."Type" = 1 and c."BackId" = a."Id") or (q."Type" = 2 and c."FrontId" = a."Id")
join cards.groups_cards gc ON gc."CardsId" = c."Id"
join cards."groups" g ON g."Id" = gc."GroupsId"
ORDER BY 1;



CREATE OR REPLACE VIEW cards.RepeatsCountSummary AS
SELECT
  COUNT(0) as "Count",
  d."NextRepeat" as "Date",
  d."OwnerId" as "OwnerId"
from cards.details d
GROUP BY d."NextRepeat", d."OwnerId"
ORDER BY d."NextRepeat";

CREATE OR REPLACE VIEW cards.cardsummary AS
 SELECT g."OwnerId",
    g."Id" AS "GroupId",
    g."Name" as "GroupName",
    c."Id" AS "CardId",
    f."Value" AS "FrontValue",
    f."Example" AS "FrontExample",
	g."Front" as "FrontLanguage",
    b."Value" AS "BackValue",
    b."Example" AS "BackExample",
	g."Back" as "BackLanguage",
    fd."Comment" AS "FrontDetailsComment",
    fd."Drawer" AS "FrontDrawer",
    fd."LessonIncluded" AS "FrontLessonIncluded",
	fd."IsTicked" AS "FrontIsTicked",
    bd."Comment" AS "BackDetailsComment",
    bd."Drawer" AS "BackDrawer",
    bd."LessonIncluded" AS "BackLessonIncluded",
	bd."IsTicked" AS "BackIsTicked"
   FROM cards.groups g
     LEFT JOIN cards.groups_cards gc ON gc."GroupsId" = g."Id"
     LEFT JOIN cards.cards c ON c."Id" = gc."CardsId"
     JOIN cards.sides f ON f."Id" = c."FrontId"
     JOIN cards.details fd ON fd."SideId" = f."Id"
     JOIN cards.sides b ON b."Id" = c."BackId"
     JOIN cards.details bd ON bd."SideId" = b."Id";


CREATE OR REPLACE VIEW cards.grouptolesson AS
SELECT
g."OwnerId" as "OwnerId",
g."Id" AS "Id",
g."Name" AS "Name",
g."Front" AS "Front",
g."Back" AS "Back",
COUNT(CASE fd."LessonIncluded" when true then null else 1 end) as "FrontCount",
COUNT(CASE bd."LessonIncluded" when true then null else 1 end) as "BackCount"
from cards."groups" g
join cards.groups_cards gc ON gc."GroupsId" = g."Id"
join cards.cards c ON c."Id" = gc."CardsId"
join cards.details fd on fd."SideId" = c."FrontId"
join cards.details bd on bd."SideId" = c."BackId"
group by g."Id"


--/var/lib/pgadmin/storage/kamilsoloducha_gmail.com/details_backup.sql
CREATE OR REPLACE VIEW cards.overview AS
SELECT 
o."Id" as "OwnerId",
COUNT(d) AS "All",
COUNT(CASE WHEN d."Drawer" = 0 THEN 1 END) AS "Drawer1",
COUNT(CASE WHEN d."Drawer" = 1 THEN 1 END) AS "Drawer2",
COUNT(CASE WHEN d."Drawer" = 2 THEN 1 END) AS "Drawer3",
COUNT(CASE WHEN d."Drawer" = 3 THEN 1 END) AS "Drawer4",
COUNT(CASE WHEN d."Drawer" = 4 THEN 1 END) AS "Drawer5",
COUNT(CASE WHEN d."LessonIncluded" THEN 1 END) AS "LessonIncluded",
COUNT(CASE WHEN d."IsTicked" THEN 1 END) AS "Ticked"
FROM cards.owners o 
LEFT JOIN cards.details d ON d."OwnerId" = o."Id"
GROUP BY o."Id"



