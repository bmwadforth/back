package database

import (
	"bmwadforth/models"
	"bmwadforth/util"
	"github.com/google/uuid"
	"github.com/lib/pq"
)

func GetArticles() ([]models.Article, error) {
	articles := make([]models.Article, 0, 10)

	db := NewDatabaseConnection()
	defer db.Database.Close()

	rows, err := db.Database.Query("SELECT * FROM articles")
	if err != nil {
		util.DatabaseError(err)
		return articles, err
	}

	for rows.Next() {
		article := models.Article{}
		err := rows.Scan(&article.ID, &article.Title, &article.Description, &article.FileRef, pq.Array(&article.Tags), &article.Created)
		if err != nil {
			util.DatabaseError(err)
			return articles, err
		}
		articles = append(articles, article)
	}

	return articles, nil
}

func GetArticle(id string) (models.Article, error) {
	article := models.Article{}

	db := NewDatabaseConnection()
	defer db.Database.Close()

	rows, err := db.Database.Query("SELECT * FROM ARTICLES WHERE ID = $1", id)
	if err != nil {
		util.DatabaseError(err)
		return article, err
	}

	for rows.Next() {
		err := rows.Scan(&article.ID, &article.Title, &article.Description, &article.FileRef, pq.Array(&article.Tags), &article.Created)
		if err != nil {
			util.DatabaseError(err)
			return article, err
		}
	}

	return article, nil
}

func NewArticle(article models.NewArticle) (uuid.UUID, error) {
	fileRef := uuid.New()
	db := NewDatabaseConnection()
	defer db.Database.Close()

	tx, err := db.Database.Begin()
	if err != nil {
		util.DatabaseError(err)
	}

	s, err := tx.Prepare("INSERT INTO ARTICLES (title, description, file_ref, tags) VALUES ($1, $2, $3, $4)")
	if err != nil {
		util.DatabaseError(err)
		return uuid.Nil, err
	}
	defer s.Close()

	_, err = s.Exec(article.Title, article.Description, fileRef, pq.Array(article.Tags))
	if err != nil {
		_ = tx.Rollback()
		util.DatabaseError(err)
		return uuid.Nil, err
	}

	err = tx.Commit()
	if err != nil {
		_ = tx.Rollback()
		util.DatabaseError(err)
		return uuid.Nil, err
	}

	return fileRef, nil
}