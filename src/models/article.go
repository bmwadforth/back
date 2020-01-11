package models

import "time"

type ArticleMeta struct {
	Views int `json:"views"`
	Likes int `json:"likes"`
}

type Article struct {
	ID          int         `json:"id"`
	Title       string      `json:"title"`
	Description string      `json:"description"`
	Tags        []string    `json:"tags"`
	Data        []byte      `json:"data"`
	Meta        ArticleMeta `json:"meta"`
	Author      User        `json:"author"`
	Created     time.Time   `json:"created"`
}
