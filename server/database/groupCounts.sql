CREATE OR REPLACE VIEW cards.GroupsCount AS
select 
count(*) as "Count",
s."UserId" as "UserId"
from cards."Groups" g
join cards."Sets" s ON s."Id" = g."CardsSetId"
group by s."UserId";


CREATE OR REPLACE VIEW cards.CardsCount AS
select 
count(*) as "Count",
s."UserId" as "UserId"
from cards."Cards" c
join cards."Groups" g ON g."Id" = c."GroupId"
join cards."Sets" s ON s."Id" = g."CardsSetId"
group by s."UserId";

