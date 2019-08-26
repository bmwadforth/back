package database

import (
	"bmwadforth/util"
	"github.com/google/uuid"
	"time"
)

type Article struct {
	Id          uint32    `json:"id"`
	Name        string    `json:"name"`
	Description string    `json:"description"`
	Data        uuid.UUID `json:"Data"`
	Tags        []string  `json:"tags"`
	Created     time.Time `json:"created"`
}

func GetArticles() ([]Article, error) {
	articles := make([]Article, 5, 10)

	db := NewDatabaseConnection()
	defer db.Database.Close()

	rows, err := db.Database.Query("SELECT * FROM ARTICLES")
	if err != nil {
		util.DatabaseError(err)
		return articles, err
	}

	for rows.Next() {
		article := Article{}
		err := rows.Scan(&article.Id, &article.Name, &article.Description, &article.Data, &article.Tags, &article.Created)
		if err != nil {
			util.DatabaseError(err)
			return articles, err
		}
		articles = append(articles, article)
	}

	return articles, nil
}

func GetArticle(id string) (Article, error) {
	article := Article{}

	db := NewDatabaseConnection()
	defer db.Database.Close()

	rows, err := db.Database.Query("SELECT * FROM ARTICLES WHERE ID = $1", id)
	if err != nil {
		util.DatabaseError(err)
		return article, err
	}

	for rows.Next() {
		err := rows.Scan(&article.Id, &article.Name, &article.Description, &article.Data, &article.Tags, &article.Created)
		if err != nil {
			util.DatabaseError(err)
			return article, err
		}
	}

	return article, nil
}
