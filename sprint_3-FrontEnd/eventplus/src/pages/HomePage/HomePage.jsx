import React, { useEffect, useState } from "react";
import "./HomePage.css";

import Banner from "../../components/Banner/Banner";
import MainContent from "../../components/MainContent/MainContent";
import VisionSection from "../../components/VisionSection/VisionSection";
import ContactSection from "../../components/ContactSection/ContactSection";
import Title from "../../components/Title/Title";
import NextEvent from "../../components/NextEvent/NextEvent";
import PreviousEvent from "../../components/PreviousEvent/PreviousEvent";
import Container from "../../components/Container/Container";
import api, {
  nextEventResource,
  previousEventResource,
} from "../../services/service";
import Notification from "../../components/Notification/Notification";

const HomePage = () => {
  const [nextEvents, setNextEvents] = useState([]);
  const [previousEvents, setPreviousEvents] = useState([]);
  const [notifyUser, setNotifyUser] = useState(); //Componente Notification

  // roda somente na inicialização do componente
  useEffect(() => {
    async function getNextEvents() {
      try {
        const promise = await api.get(nextEventResource);
        const dados = await promise.data;

        setNextEvents(dados);
      } catch (error) {
        console.log("não trouxe os próximos eventos, verifique lá!");
      }
    }

    async function getPreviousEvents() {
      try {
        const promise = await api.get(previousEventResource);
        const dados = await promise.data;

        setPreviousEvents(dados);
      } catch (error) {
        console.log("não trouxe os eventos anteriores, verifique lá!");
      }
    }

    getNextEvents();
    getPreviousEvents();
  }, []);

  return (
    <MainContent>
      {<Notification {...notifyUser} setNotifyUser={setNotifyUser} />}
      <Banner />

      {/* PRÓXIMOS EVENTOS */}
      <section className="proximos-eventos">
        <Container>
          <Title titleText={"Próximos Eventos"} />

          <div className="events-box">
            {nextEvents.map((e) => {
              return (
                <NextEvent
                  key={e.idEvento}
                  title={e.nomeEvento}
                  description={e.descricao}
                  eventDate={e.dataEvento}
                  idEvent={e.idEvento}
                />
              );
            })}
          </div>

          <Title titleText={"Eventos Anteriores"} />

          <div className="events-box">
            {previousEvents.map((e) => {
              return (
                <PreviousEvent
                  key={e.idEvento}
                  title={e.nomeEvento}
                  description={e.descricao}
                  eventDate={e.dataEvento}
                  idEvent={e.idEvento}
                />
              );
            })}

          </div>
        </Container>
      </section>

      <VisionSection />
      <ContactSection />
    </MainContent>
  );
};

export default HomePage;
