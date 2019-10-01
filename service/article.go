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

func GetArticle(id uuid.UUID) ([]byte, error){
	bytes, err := getFileFromS3(id)
	if err != nil {
		return nil, err
	}

	return bytes, nil
}
