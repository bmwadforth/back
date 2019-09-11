package service

import (
	"bytes"
	"github.com/aws/aws-sdk-go/aws"
	"github.com/aws/aws-sdk-go/aws/session"
	"github.com/aws/aws-sdk-go/service/s3"
	"github.com/google/uuid"
	"mime/multipart"
	"net/http"
)

func addFileToS3(objectKey uuid.UUID, fh *multipart.FileHeader) (bool, error){
	file, err := fh.Open()
	if err != nil {
		return false, err
	}

	buffer := make([]byte, fh.Size)
	_, err = file.Read(buffer)
	if err != nil {
		return false, err
	}

	sess := session.Must(session.NewSession(&aws.Config{Region: aws.String("ap-southeast-2")}))
	svc := s3.New(sess)
	_, err = svc.PutObject(&s3.PutObjectInput{
		Bucket:               aws.String("bmwadforth"),
		Key:                  aws.String(objectKey.String()),
		ACL:                  aws.String("private"),
		Body:                 bytes.NewReader(buffer),
		ContentLength:        aws.Int64(fh.Size),
		ContentType:          aws.String(http.DetectContentType(buffer)),
		ContentDisposition:   aws.String("attachment"),
		ServerSideEncryption: aws.String("AES256"),
	})
	if err != nil {
		return false, err
	}

	return true, nil
}