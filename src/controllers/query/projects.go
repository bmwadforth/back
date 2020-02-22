package query

import (
	"errors"
	"github.com/bmwadforth/back/src/controllers/types"
	"github.com/bmwadforth/back/src/database"
	"github.com/graphql-go/graphql"
)

var ProjectsQuery = &graphql.Field{
	Type: graphql.NewList(types.ProjectType),
	Resolve: func(p graphql.ResolveParams) (interface{}, error) {
		projects, err := database.GetProjects()
		if err != nil {
			return nil, errors.New("error searching articles")
		}
		return projects, nil
	},
}
