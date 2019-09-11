package service

import (
	"github.com/google/uuid"
	"mime/multipart"
)

func NewArticle(id uuid.UUID, data *multipart.FileHeader) (bool, error){
	_, err := addFileToS3(id, data)
	if err != nil {
		return false, err
	}

	return true, nil
}