
CREATE SCHEMA IF NOT EXISTS lessons;

CREATE TABLE lessons."Performances" (
    "Id" uuid NOT NULL,
    "UserId" uuid NOT NULL,
    CONSTRAINT "PK_Performances" PRIMARY KEY ("Id")
);

CREATE TABLE lessons."Lessons" (
    "StartDate" timestamp without time zone NOT NULL,
    "UserId" uuid NOT NULL,
    "Type" integer NOT NULL,
    "TimeCounter" integer NOT NULL,
    "PerformenceId" uuid NULL,
    CONSTRAINT "PK_Lessons" PRIMARY KEY ("UserId", "StartDate"),
    CONSTRAINT "FK_Lessons_Performances_PerformenceId" FOREIGN KEY ("PerformenceId") REFERENCES lessons."Performances" ("Id") ON DELETE RESTRICT
);

CREATE INDEX "IX_Lessons_PerformenceId" ON lessons."Lessons" ("PerformenceId");
