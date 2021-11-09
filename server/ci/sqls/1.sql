use Wordki_Dev;

CREATE TABLE users (
    id                BIGINT AUTO_INCREMENT PRIMARY KEY,
    name              VARCHAR(50) UNIQUE NOT NULL,
    password          VARCHAR(32) NOT NULL,
    creationDate      DATE NOT NULL,
    lastLoginDate     DATETIME
);

CREATE TABLE groups2 (
    id                BIGINT AUTO_INCREMENT PRIMARY KEY,
    userId            BIGINT NOT NULL,
    name              VARCHAR(50) NOT NULL,
    language1         INT NOT NULL,
    language2         INT NOT NULL,
    creationDate      DATETIME NOT NULL,
    FOREIGN KEY (userId) REFERENCES users(id) ON DELETE CASCADE
);

CREATE TABLE words (
    id                BIGINT AUTO_INCREMENT PRIMARY KEY,
    groupId           BIGINT NOT NULL,
    language1         VARCHAR(100),
    language2         VARCHAR(100),
    example1          VARCHAR(100),
    example2          VARCHAR(100),
    comment           VARCHAR(100),
    drawer            INT NOT NULL,
    isVisible         TINYINT(1) NOT NULL,
    nextRepeat        DATETIME NOT NULL,
    creationDate      DATETIME NOT NULL,
    FOREIGN KEY (groupId) REFERENCES groups2(id) ON DELETE CASCADE
);

CREATE TABLE repeats (
    id                BIGINT AUTO_INCREMENT PRIMARY KEY,
    wordId            BIGINT NOT NULL,
    result            TINYINT(1) NOT NULL,
    date              DATETIME,
    FOREIGN KEY (wordId) REFERENCES words(id) ON DELETE CASCADE
);
