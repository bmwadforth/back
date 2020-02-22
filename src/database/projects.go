package database

import (
	"github.com/bmwadforth/back/src/models"
	"github.com/lib/pq"
)

func GetProjects() ([]models.Project, error) {
	projects := make([]models.Project, 0, 10)

	db := OpenDatabase()

	rows, err := db.Database.Query("SELECT * FROM BLOG.PROJECTS ORDER BY created DESC;")
	if err != nil {
		return nil, err
	}

	for rows.Next() {
		project := models.Project{}
		err := rows.Scan(&project.ID, &project.Title, &project.Description, pq.Array(&project.Tags), &project.Github, &project.Created)
		if err != nil {
			return nil, err
		}
		projects = append(projects, project)
	}

	return projects, nil
}
