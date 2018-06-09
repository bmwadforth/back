package models

type Blog struct {
	BlogId          int    `json:"id"`
	BlogTitle       string `json:"title"`
	BlogContent     string `json:"content"`
	BlogDescription string `json:"description"`
	BlogViews       string `json:"views"`
	BlogCreatedAt   string `json:"createdOn"`
}
