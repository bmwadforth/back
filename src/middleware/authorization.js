import buildResponse from '../util/response';

export function authorize(req, res, next) {
  const authHeader = req.get('Authorization');
  //TODO: This shouldn't be my_key when deployed, revert back to process.env
  const authKey = 'my_key' || process.env.AUTHORIZATION_KEY;

  if (!authHeader) {
    res.status(401).json(buildResponse('UNAUTHORIZED', null, 'UNAUTHORIZED'));
    return;
  }

  const splitHeader = authHeader.split('Bearer ');
  if (splitHeader.length !== 2) {
    res.status(401).json(buildResponse('UNAUTHORIZED', null, 'UNAUTHORIZED'));
    return;
  }

  if (splitHeader[1] !== authKey) {
    res.status(401).json(buildResponse('UNAUTHORIZED', null, 'UNAUTHORIZED'));
    return;
  }

  next();
}
