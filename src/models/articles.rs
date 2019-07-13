use serde::Serialize;
use uuid::Uuid;
use chrono::NaiveDateTime;

#[derive(Serialize)]
pub struct Article {
    pub id: i32,
    pub name: String,
    pub content_key: Uuid,
    pub content: Option<ArticleContent>,
    pub tags: Option<Vec<String>>,
    pub created: NaiveDateTime
}

#[derive(Serialize)]
pub struct ArticleContent {
    pub data: String
}