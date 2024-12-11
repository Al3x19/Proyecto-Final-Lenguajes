import * as Yup from 'yup';

export const userFormInitValues = {
  email: 'blah',
  userName: '',
  password: '',
  confirmPassword: '',
};

export const userValidationSchema = Yup.object({

  email: Yup.string()
    .email('El campo debe ser un correo electrónico válido.')
    .required('El campo Correo Electrónico es requerido.'),

  userName: Yup.string()
    .min(3, 'El Nombre de usuario debe tener al menos 3 caracteres.')
    .required('El campo Nombre de usuario es requerido.'),
 
    password: Yup.string()
    .required('El campo Contraseña es requerido.')
    .min(8, 'La contraseña debe tener al menos 8 caracteres.')
    .matches(/[a-z]/, 'La contraseña debe contener al menos una letra minúscula.')
    .matches(/[A-Z]/, 'La contraseña debe contener al menos una letra mayúscula.')
    .matches(/\d/, 'La contraseña debe contener al menos un número.')
    .matches(/[@$!%*?&]/, 'La contraseña debe contener al menos un carácter especial.'),
  
  confirmPassword: Yup.string()
    .oneOf([Yup.ref('password'), null], 'Las contraseñas deben coincidir.')
    .required('El campo Confirmar Contraseña es requerido.'),
});


