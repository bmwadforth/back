#![feature(proc_macro_hygiene, decl_macro)]
#[macro_use]
extern crate rocket;
extern crate serde;
extern crate postgres;
extern crate time;
extern crate uuid;

mod controllers;
mod data;
mod models;
mod fairings;
mod service;

fn main() {
    rocket::ignite().attach(fairings::cors::Cors).mount("/", routes![controllers::articles::fetch_articles, controllers::articles::fetch_article]).launch();
}