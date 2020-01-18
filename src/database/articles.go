package database

import (
	"errors"
	"github.com/bmwadforth/back/src/models"
	"github.com/lib/pq"
	"log"
)

func GetArticles() ([]models.Article, error) {
	articles := make([]models.Article, 0, 10)

	db := OpenDatabase()
	defer db.Database.Close()

	rows, err := db.Database.Query("SELECT article_id, article_title, article_description, article_tags, article_created, author_id, author_first_name, author_last_name, author_created FROM BLOG.v_articles WHERE ARTICLE_STATUS = 'ACTIVE'::BLOG.ARTICLE_STATUS;")
	if err != nil {
		return nil, err
	}

	for rows.Next() {
		article := models.Article{Author: models.Author{}, Meta: models.ArticleMeta{Likes: 0, Views: 0}}
		err := rows.Scan(&article.ID, &article.Title, &article.Description, pq.Array(&article.Tags), &article.Created, &article.Author.ID, &article.Author.FirstName, &article.Author.LastName, &article.Author.Created)
		if err != nil {
			return nil, err
		}
		articles = append(articles, article)
	}

	return articles, nil
}

func NewArticle(title string, description string, data []byte, tags []string, author int) error {
	instance := OpenDatabase()
	db := instance.Database
	defer db.Close()

	tx, err := db.Begin()
	if err != nil {
		log.Println(err)
		return errors.New("unable to create database transaction")
	}

	_, err = tx.Exec("INSERT INTO BLOG.ARTICLES(title, description, data, tags, author) VALUES ($1, $2, $3, $4, $5);", title, description, data, pq.Array(tags), author)
	if err != nil {
		log.Println(err)

		err = tx.Rollback()
		if err != nil {
			log.Println(err)
			return errors.New("unable to rollback database action")
		}

		return errors.New("unable to execute database action")
	}

	// commit the transaction
	err = tx.Commit()
	if err != nil {
		log.Println(err)
		return errors.New("unable to commit database action")
	}

	return nil

}
