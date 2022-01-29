CREATE OR REPLACE VIEW card.Repeats AS
SELECT 
random() AS "Random",
COALESCE(f."Id", b."Id") AS "CardId",
d."OwnerId" as "OwnerId",
d."NextRepeat" as "NextRepeat",
-- d."LessonIncluded" as "LessonIncluded",
COALESCE(d."Value", s."Value") AS "Value",
COALESCE(d."Example", s."Example") AS "Example",
s."Type",
s."Comment",
g."Front",
g."Back",
g."Name"
FROM cards.details d
JOIN cards.sides s ON s."Id" = d."SideId"
LEFT JOIN cards.cards b ON b."BackId" = s."Id"
LEFT JOIN cards.cards f ON f."FrontId" = s."Id"
join cards.groups_cards gc ON gc."CardsId" = COALESCE(f."Id", b."Id")
join cards."groups" g on g."Id" = gc."GroupsId" and g."OwnerId" = d."OwnerId"
WHERE d."LessonIncluded" = true
ORDER BY 1;

