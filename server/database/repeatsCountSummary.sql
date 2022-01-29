CREATE OR REPLACE VIEW cards.RepeatsCountSummary AS
SELECT
COUNT(0) as "Count",
d."NextRepeat" as "Date",
d."OwnerId" as "OwnerId"
from cards.details d
GROUP BY d."NextRepeat", d."OwnerId"
ORDER BY d."NextRepeat"