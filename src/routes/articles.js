import express from 'express';
import buildResponse from '../util/response';

const router = express.Router();

router.get('/', function(req, res, next) {
  res.json(buildResponse("Fetched articles successfully", []));
});

/*router.put('/', (req, res, next) => {
  res.json(buildResponse("Article created successfully", []));
})*/

export default router;