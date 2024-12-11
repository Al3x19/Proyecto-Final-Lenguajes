import * as Yup from "yup";

export const softwareValidationSchema = Yup.object().shape({
  Name: Yup.string()
    .required("El nombre es requerido."),
  
  Description: Yup.string()
    .min(10, "La descripción debe tener al menos 10 caracteres.")
    .max(500, "La descripción no puede tener más de 500 caracteres.")
    .required("La descripción es requerida."),
  
  Price: Yup.number()
    .typeError("El precio debe ser un número.")
    .required("El precio es requerido.")
    .positive("El precio debe ser un número positivo."),
  
  Icon: Yup.string()
    .nullable()
    .url("El icono debe ser una URL válida.")
    .required("El icono es requerido."),
  
  Version: Yup.string()
    .matches(/^\d+\.\d+\.\d+$/, "El formato de versión no es válido.")
    .required("La versión es requerida."),
  
  TagList: Yup.array()
    .of(Yup.string())
    .min(1, "Debe haber al menos un tag.")
    .required("La lista de tags es requerida."),
});