use rocket_contrib::json::Json;
use crate::models::articles::{Article, ArticleContent};
use crate::data::articles::get_articles;
use crate::data::articles::get_article;
use crate::service::s3::get_article_content;
use rocket::Response;

#[get("/api/articles")]
pub fn fetch_articles() -> Json<Vec<Article>> {
    Json(get_articles())
}

#[get("/api/article?<name>")]
pub fn fetch_article(name: String) -> Option<Json<Article>> {
    let article = get_article(name);
    match article {
        Some(mut article) => {
            let article_content = get_article_content(article.content_key.to_string());
            article.content = Some(article_content);
            return Some(Json(article))
        },
        None => return None
    }
}

