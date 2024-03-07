import { RegisterFormModel } from '../models'

export function validate(values: RegisterFormModel): RegisterFormModel {
  const errors = {} as RegisterFormModel
  if (!values.userName?.length) {
    errors.userName = 'Field is required'
  }
  if (!values.email?.length) {
    errors.email = 'Field is required'
  }
  if (!values.password?.length) {
    errors.password = 'Field is required'
  }
  if (!values.passwordConfirmation?.length) {
    errors.passwordConfirmation = 'Field is required'
  }
  if (values.passwordConfirmation !== values.password) {
    errors.passwordConfirmation = 'Password must be exactly the same'
  }

  return errors
}
