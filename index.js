import express from 'express';
import cookieParser from 'cookie-parser';
import logger from './src/util/logger';
import buildResponse from './src/util/response';
import articleRouter from './src/routes/articles.js';
import commentsRouter from './src/routes/comment.js';
import { connectDatabase } from './src/data';

(async function () {
  try {
    await connectDatabase();

    const app = express();

    app.use(express.json());
    app.use(express.urlencoded({ extended: false }));
    app.use(cookieParser());

    // Logging handler
    app.use((req, res, next) => {
      next();
      if (res.statusCode >= 400) {
        logger.error(`Path: ${req.originalUrl} Status: ${res.statusCode}`);
      } else {
        logger.info(`Path: ${req.originalUrl} Status: ${res.statusCode}`);
      }
    });

    app.use('/api/v1/articles', articleRouter);
    app.use('/api/v1/comments', commentsRouter);

    // Error handler
    app.use(function (err, req, res, next) {
      logger.error('An error occured');
      console.error(err);
      res.status(500).json(buildResponse('INTERNAL SERVER ERROR', null, 'INTERNAL SERVER ERROR'));
    });

    // Not found handler
    app.use(function (req, res, next) {
      res.status(404).json(buildResponse('NOT FOUND', null, 'NOT FOUND'));
    });

    const PORT = process.env.PORT || 8080;
    app.listen(PORT, () => {
      console.log(`App listening on: ${PORT}`);
      console.log('Press Ctrl+C to quit.');
    });
  } catch (error) {
    logger.error(error);
  }
})();
