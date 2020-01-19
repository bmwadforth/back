package database

import (
	"errors"
	"github.com/bmwadforth/back/src/models"
	"github.com/lib/pq"
	"log"
	"strings"
)

func GetArticles() ([]models.Article, error) {
	articles := make([]models.Article, 0, 10)

	db := OpenDatabase()

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

func GetArticle(id int) (models.Article, error) {
	article := models.Article{Author: models.Author{}, Meta: models.ArticleMeta{Likes: 0, Views: 0}}
	db := OpenDatabase()

	rows, err := db.Database.Query("SELECT article_id, article_title, article_description, article_data, article_tags, article_created, author_id, author_first_name, author_last_name, author_created FROM BLOG.v_articles WHERE ARTICLE_STATUS = 'ACTIVE'::BLOG.ARTICLE_STATUS AND article_id = $1;", id)
	if err != nil {
		log.Println(err)
		return article, err
	}

	err = ViewArticle(id)
	if err != nil {
		log.Println(err)
	}

	for rows.Next() {
		err := rows.Scan(&article.ID, &article.Title, &article.Description, &article.Data, pq.Array(&article.Tags), &article.Created, &article.Author.ID, &article.Author.FirstName, &article.Author.LastName, &article.Author.Created)
		if err != nil {
			log.Println(err)
			return article, err
		}
	}

	return article, nil
}

func SearchArticles(keywords []string) ([]models.Article, error) {
	articles := make([]models.Article, 0, 10)

	joinedKeyword := strings.Join(keywords, " | ")

	db := OpenDatabase()
	rows, err := db.Database.Query("WITH articles AS (SELECT * FROM BLOG.V_ARTICLES) SELECT article_id, article_title, article_description, article_tags, article_created, author_id, author_first_name, author_last_name, author_created FROM (SELECT article_id, article_title, article_description, article_tags, article_created, article_status, author_id, author_first_name, author_last_name, author_created, (to_tsvector('english', (convert_from(decode(article_data, 'base64'), 'UTF8') || ' ' || article_title || ' ' || article_description || ' ' || array_to_string(article_tags, ' '))) @@ to_tsquery('english', $1)) as match FROM articles) as a WHERE match = true AND article_status = 'ACTIVE'::BLOG.ARTICLE_STATUS;", joinedKeyword)
	if err != nil {
		log.Println(err)
		return nil, err
	}

	for rows.Next() {
		article := models.Article{Author: models.Author{}, Meta: models.ArticleMeta{Likes: 0, Views: 0}}
		err := rows.Scan(&article.ID, &article.Title, &article.Description, pq.Array(&article.Tags), &article.Created, &article.Author.ID, &article.Author.FirstName, &article.Author.LastName, &article.Author.Created)
		if err != nil {
			log.Println(err)
			return nil, err
		}
		articles = append(articles, article)
	}

	return articles, nil
}

func NewArticle(title string, description string, data []byte, tags []string, author int) error {
	instance := OpenDatabase()
	db := instance.Database

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

	err = tx.Commit()
	if err != nil {
		log.Println(err)
		return errors.New("unable to commit database action")
	}

	return nil
}

func ViewArticle(id int) error {
	instance := OpenDatabase()
	db := instance.Database

	tx, err := db.Begin()
	if err != nil {
		log.Println(err)
		return errors.New("unable to create database transaction")
	}

	_, err = tx.Exec("UPDATE BLOG.ARTICLES SET meta = jsonb_set(meta, '{views}', (COALESCE(meta->>'views','0')::int + 1)::text::jsonb) WHERE identifier = $1;", id)
	if err != nil {
		log.Println(err)

		err = tx.Rollback()
		if err != nil {
			log.Println(err)
			return errors.New("unable to rollback database action")
		}

		return errors.New("unable to execute database action")
	}

	err = tx.Commit()
	if err != nil {
		log.Println(err)
		return errors.New("unable to commit database action")
	}

	return nil
}
