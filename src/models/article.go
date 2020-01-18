package models

import "time"

type ArticleMeta struct {
	Views int `json:"views"`
	Likes int `json:"likes"`
}

type ArticleStatus string

const (
	DRAFT    ArticleStatus = "DRAFT"
	ACTIVE   ArticleStatus = "ACTIVE"
	ARCHIVED ArticleStatus = "ARCHIVED"
)

type Article struct {
	ID          int           `json:"id"`
	Title       string        `json:"title"`
	Description string        `json:"description"`
	Tags        []string      `json:"tags"`
	Data        []byte        `json:"data"`
	Status      ArticleStatus `json:"status"`
	Meta        ArticleMeta   `json:"meta"`
	Author      Author        `json:"author"`
	Created     time.Time     `json:"created"`
}
