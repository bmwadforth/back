use rusoto_core::Region;
use rusoto_s3::{S3Client, S3, GetObjectRequest, GetObjectOutput};
use crate::models::articles::ArticleContent;
use std::error::Error;
use std::io::Read;

pub fn get_article_content(key: String) -> ArticleContent {
    let client = S3Client::new(Region::ApSoutheast2);
    let object_request = GetObjectRequest {
        bucket: String::from("bmwadforth"),
        key,
        ..Default::default()
    };
    let future = client.get_object(object_request);
    //Todo: Update this so it isn't a synchronous future, rather an asynchronous one
    let result = future.sync().unwrap();

    let body = result.body.unwrap();
    let content_length = result.content_length.unwrap();
    let mut buff = String::new();
    let bytes_read = body.into_blocking_read().read_to_string(&mut buff);

    return ArticleContent{data: buff};
}