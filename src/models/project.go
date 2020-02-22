package models

import "time"

type Project struct {
	ID          int       `json:"id"`
	Title       string    `json:"title"`
	Description string    `json:"description"`
	Tags        []string  `json:"tags"`
	Github		string	  `json:"github"`
	Created     time.Time `json:"created"`
}
