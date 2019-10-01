package models

import (
	"github.com/google/uuid"
	"time"
)

type NewArticle struct {
	Title       string    `json:"title"`
	Description string    `json:"description"`
	Tags        []string  `json:"tags"`
}

type Article struct {
	ID          uint32    `json:"id"`
	Title       string    `json:"title"`
	Description string    `json:"description"`
	FileRef     uuid.UUID `json:"fileRef"`
	FileContent	string    `json:"fileContent"`
	Tags        []string  `json:"tags"`
	Created     time.Time `json:"created"`
}