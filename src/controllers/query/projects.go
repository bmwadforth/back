package query

import (
	"github.com/bmwadforth/back/src/controllers/types"
	"github.com/graphql-go/graphql"
)

var ProjectsQuery = &graphql.Field{
	Type: graphql.NewList(types.ProjectType),
	Resolve: func(p graphql.ResolveParams) (interface{}, error) {

		return nil, nil
	},
}
