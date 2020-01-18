CREATE DATABASE BMWADFORTH;

CREATE SCHEMA BLOG;

CREATE TABLE BLOG.AUTHORS
(
    IDENTIFIER serial primary key unique not null,
    FIRST_NAME varchar(128)              not null,
    LAST_NAME  varchar(128)              not null,
    USERNAME   varchar(128)              not null,
    PASSWORD   varchar(128)              not null,
    CREATED    timestamptz               not null default current_timestamp
);

CREATE UNIQUE INDEX IDX_AUTHORS_USERNAME
    ON BLOG.AUTHORS (USERNAME);

CREATE TYPE BLOG.ARTICLE_STATUS AS ENUM ('DRAFT', 'ACTIVE', 'ARCHIVED');
CREATE TABLE BLOG.ARTICLES
(
    IDENTIFIER  serial primary key unique                not null,
    TITLE       varchar(128)                             not null unique,
    DESCRIPTION varchar(255)                             not null,
    DATA        text                                     not null,
    TAGS        text[]                                   not null default array []::text[],
    META        jsonb                                    not null default '{}'::jsonb,
    STATUS      BLOG.ARTICLE_STATUS                      not null default 'DRAFT'::BLOG.ARTICLE_STATUS,
    AUTHOR      int references BLOG.AUTHORS (IDENTIFIER) not null,
    CREATED     timestamptz                              not null default current_timestamp
);

CREATE UNIQUE INDEX IDX_ARTICLES_TITLE
    ON BLOG.ARTICLES (TITLE);

CREATE INDEX IDX_ARTICLES_DATA
    ON BLOG.ARTICLES USING gin (to_tsvector('english', DATA))