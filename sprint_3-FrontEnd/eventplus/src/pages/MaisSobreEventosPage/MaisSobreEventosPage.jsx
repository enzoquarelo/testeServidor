import React, { useEffect, useState, useContext } from "react";
import "./MaisSobreEventosPage.css";
import Title from "../../components/Title/Title";
import MainContent from "../../components/MainContent/MainContent";
import TableEvent from "./TableEvent/TableEvent";
import Container from "../../components/Container/Container";

import api, { comentaryEventResource } from "../../services/service";
import Spinner from "../../components/Spinner/Spinner";
import { useParams } from "react-router-dom";
import { UserContext } from "../../context/AuthContext";

const MaisSobreEventosPage = () => {
  const [eventos, setEventos] = useState([]);
  const [showSpinner, setShowSpinner] = useState(false);

  const { idEvent } = useParams();
  const { userData } = useContext(UserContext);


  useEffect(() => {

    async function loadEvents() {
      setShowSpinner(true);
  
      try {
        const response = await api.get(`${comentaryEventResource}/${idEvent}`);
        const eventosData = response.data;
        setEventos(eventosData);
      } catch (error) {
        console.error("Erro na API", error);
      }
  
      setShowSpinner(false);
    }
    console.log(userData);

    loadEvents();
  }, [idEvent]);

  return (
    <MainContent>
      <Container>
        {showSpinner ? (
          <Spinner />
        ) : (
          <TableEvent dados={eventos} />
        )}
      </Container>
    </MainContent>
  );
};

export default MaisSobreEventosPage;