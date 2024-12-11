import { Carousel } from "../components";
export { Header } from "../components";
import { Link } from "react-router-dom";

export const HomePage = () => {
  return (
    <div className="bg-gray-100 w-full flex flex-col items-center content-center">
      <div className="w-full h-auto bg-gray-200 px-6 py-4 flex justify-center border-b-2 border-black">
        <div className="text-center mb-5">
          <h3 className="text-3xl sm:text-4xl leading-normal font-extrabold tracking-tight text-gray-900">
            Tienda <span className="text-gray-700">Software</span>
          </h3>
        </div>
      </div>
      <Carousel />
      <div className="w-full h-auto m-2 bg-gray-200">
        <section className="pt-20 lg:pt-[120px] pb-10 lg:pb-20 bg-[#F3F4F6]">
          <div className="container">
            <div className="flex flex-wrap mx-4">
              <div className="w-full md:w-1/2 xl:w-1/3 px-4 hover:bg-gray-50">
                <div className="bg-white rounded-lg overflow-hidden mb-10  hover:bg-gray-100">
                  <img
                    src="https://cdn.tailgrids.com/1.0/assets/images/cards/card-01/image-01.jpg"
                    alt="image"
                    className="w-full"
                  />
                  <div className="p-8 sm:p-9 md:p-7 xl:p-9 text-center">
                    <h3>
                    <Link
                        href="#"
                        className="
                      font-semibold
                      text-dark text-xl
                      sm:text-[22px]
                      md:text-xl
                      lg:text-[22px]
                      xl:text-xl
                      2xl:text-[22px]
                      mb-4
                      block
                      hover:text-primary
                      "
                      >
                        Estos son nuestros software más descargados
                      </Link>
                    </h3>
                    <p className="text-base text-body-color leading-relaxed mb-7 text-justify">
                    Descubre las herramientas favoritas de nuestros usuarios. 
                    Esta selección reúne los software más descargados, reconocidos 
                    por su calidad, rendimiento y utilidad. ¡No te quedes atrás y 
                    pruébalos tú también!
                    </p>
                    <Link
                        to=""
                        className="
                     inline-block
                     py-2
                     px-7
                     border border-[#E5E7EB]
                     rounded-full
                     text-base text-body-color
                     font-medium
                     hover:border-primary hover:bg-primary hover:text-white
                     transition
                     "
                      >
                        View Details
                      </Link>
                  </div>
                </div>
              </div>
              <div className="w-full md:w-1/2 xl:w-1/3 px-4 hover:bg-gray-50">
                <div className="bg-white rounded-lg overflow-hidden mb-10  hover:bg-gray-100">
                  <img
                    src="https://cdn.tailgrids.com/1.0/assets/images/cards/card-01/image-02.jpg"
                    alt="image"
                    className="w-full"
                  />
                  <div className="p-8 sm:p-9 md:p-7 xl:p-9 text-center">
                    <h3>
                    <Link
                        href="#"
                        className="
                      font-semibold
                      text-dark text-xl
                      sm:text-[22px]
                      md:text-xl
                      lg:text-[22px]
                      xl:text-xl
                      2xl:text-[22px]
                      mb-4
                      block
                      hover:text-primary
                      "
                      >
                        Nuestros software más populares
                      </Link>
                    </h3>
                    <p className="text-base text-body-color leading-relaxed mb-7 text-justify">
                    Explora las soluciones más vistas y destacadas por nuestra comunidad. 
                    Cada software en esta selección ha sido diseñado para ofrecerte eficiencia, 
                    funcionalidad y resultados excepcionales. ¡Descubre por qué son los favoritos 
                    de nuestros usuarios!
                    </p>
                    <Link
                        to=""
                        className="
                     inline-block
                     py-2
                     px-7
                     border border-[#E5E7EB]
                     rounded-full
                     text-base text-body-color
                     font-medium
                     hover:border-primary hover:bg-primary hover:text-white
                     transition
                     "
                      >
                        View Details
                      </Link>
                  </div>
                </div>
              </div>
              <div className="w-full md:w-1/2 xl:w-1/3 px-4 hover:bg-gray-50">
                <div className="bg-white rounded-lg overflow-hidden mb-10  hover:bg-gray-100">
                  <img
                    src="https://cdn.tailgrids.com/1.0/assets/images/cards/card-01/image-03.jpg"
                    alt="image"
                    className="w-full"
                  />
                  <div className="p-8 sm:p-9 md:p-7 xl:p-9 text-center">
                    <h3>
                      <Link
                        href="#"
                        className="
                      font-semibold
                      text-dark text-xl
                      sm:text-[22px]
                      md:text-xl
                      lg:text-[22px]
                      xl:text-xl
                      2xl:text-[22px]
                      mb-4
                      block
                      hover:text-primary
                      "
                      >
                        Nuestros software más exclusivos, diseñados especialmente para ti
                      </Link>
                    </h3>
                    <p className="text-base text-body-color leading-relaxed mb-7 text-justify">
                    Encuentra herramientas innovadoras y diseñadas para ofrecer soluciones premium. 
                    Cada software está cuidadosamente seleccionado para adaptarse a tus necesidades 
                    y ayudarte a alcanzar tus objetivos con eficiencia.
                    </p>
                    <Link
                        to=""
                        className="
                     inline-block
                     py-2
                     px-7
                     border border-[#E5E7EB]
                     rounded-full
                     text-base text-body-color
                     font-medium
                     hover:border-primary hover:bg-primary hover:text-white
                     transition
                     "
                      >
                        View Details
                      </Link>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </section>
      </div>
    </div>
  );
};
